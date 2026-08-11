using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Facebook.Web.Mvc;
using Facebook.Api.DemoSite.Models.Demo;
using Newtonsoft.Json;

namespace Facebook.Api.DemoSite.Controllers
{
    public partial class DemoController : FacebookMvcController
    {
        public ActionResult Admin()
        {
            AdminIndexData model = null;
            using (var batch = Batch.Start(this.FbContext))
            {
                var notificationsPerDayResult = this.FbContext.Admin.GetAllocation("notifications_per_day");
                var announcementNotificationsPerWeekResult = this.FbContext.Admin.GetAllocation("announcement_notifications_per_week");
                var requestsPerDayResult = this.FbContext.Admin.GetAllocation("requests_per_day");
                var emailsPerDayResult = this.FbContext.Admin.GetAllocation("emails_per_day");
                var emailDisableMessageLocationResult = this.FbContext.Admin.GetAllocation("email_disable_message_location");

                var appProperties = new String[] {
	                "about_url"/*,
	                "app_id",
	                "application_name",
	                "authorize_url",
	                "base_domain",
	                "callback_url",
	                "canvas_name",
	                "connect_logo_url",
	                "connect_url",
	                "dashboard_url",
	                "default_action_fbml",
	                "default_column",
	                "default_fbml",
	                "description",
	                "desktop",
	                "dev_mode",
	                "edit_url",
	                "email",
	                "help_url",
	                "icon_url",
	                "info_changed_url",
	                "installable",
	                "ip_list",
	                "is_mobile",
	                "logo_url",
	                "message_action",
	                "message_url",
	                "post_authorize_redirect_url",
	                "post_install_url",
	                "preload_fql",
	                "privacy_url",
	                "private_install",
	                "profile_tab_url",
	                "publish_action",
	                "publish_self_action",
	                "publish_self_url",
			        "publish_url",
	                "see_all_url",
	                "tab_default_name",
	                "tos_url",
	                "uninstall_url",
	                "use_iframe",
	                "wise_mode"*/
                };
                //var getAppPropertiesResult = this.FbContext.Admin.GetAppProperties(appProperties);

                var metrics = new String[] {
	                "active_users",
	                "api_calls",
	                "unique_api_calls",
	                "canvas_page_views",
	                "unique_canvas_page_views",
	                "canvas_http_request_time_avg",
	                "canvas_fbml_render_time_avg",
	                "unique_blocks",
	                "unique_unblocks",
	                "canvas_page_views_http_code_0",
	                "canvas_page_views_http_code_100",
	                "canvas_page_views_http_code_200",
	                "canvas_page_views_http_code_200ND",
	                "canvas_page_views_http_code_301",
	                "canvas_page_views_http_code_302",
	                "canvas_page_views_http_code_303",
	                "canvas_page_views_http_code_400",
	                "canvas_page_views_http_code_401",
	                "canvas_page_views_http_code_403",
	                "canvas_page_views_http_code_404",
	                "canvas_page_views_http_code_405",
	                "canvas_page_views_http_code_413",
	                "canvas_page_views_http_code_422",
	                "canvas_page_views_http_code_500",
	                "canvas_page_views_http_code_502",
	                "canvas_page_views_http_code_503",
	                "canvas_page_views_http_code_505"
                };
                var getMetricsResult = this.FbContext.Admin.GetMetrics(DateTime.Now.AddDays(-7), DateTime.Now, ApplicationMetricPeriod.Day, metrics);

                var getRestrictionInfoResult = this.FbContext.Admin.GetRestrictionInfo();

                batch.Complete();

                model = new AdminIndexData
                {
                    NotificationsPerDay = notificationsPerDayResult.Value,
                    AnnoucementNotificationsPerWeek = announcementNotificationsPerWeekResult.Value,
                    RequestsPerDay = requestsPerDayResult.Value,
                    EmailsPerDay = emailsPerDayResult.Value,
                    EmailDisableMessageLocation = emailDisableMessageLocationResult.Value,
                    //AppProperties = JsonConvert.DeserializeObject<Dictionary<String, String>>(getAppPropertiesResult.Value),
                    Metrics = getMetricsResult.Value,
                    RestrictionInfo = getRestrictionInfoResult.Value
                };
            }

            return View(model);
        }
    }
}