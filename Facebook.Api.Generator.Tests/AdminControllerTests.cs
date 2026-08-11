using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Desktop;

namespace Facebook.Api.Generator.Tests
{
    /// <summary>
    /// Summary description for AdminControllerTests
    /// </summary>
    [TestClass]
    public class AdminControllerTests
    {
        public AdminControllerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        public static void GetAllocationHelper(ref FacebookDesktopContext context, String allocation)
        {
            var result = context.Admin.GetAllocation(allocation);
            Assert.IsFalse(result.IsError, "GetAllocation for {0} failed: {1}", allocation, result.IsError ? result.ResponseException.Message : String.Empty);
        }

        [TestMethod]
        public void GetAllocationTest()
        {
            var fb = Helpers.GetDesktopContext();            

            AdminControllerTests.GetAllocationHelper(ref fb, "notifications_per_day");
            AdminControllerTests.GetAllocationHelper(ref fb, "announcement_notifications_per_week");
            AdminControllerTests.GetAllocationHelper(ref fb, "requests_per_day");
            AdminControllerTests.GetAllocationHelper(ref fb, "emails_per_day");
            AdminControllerTests.GetAllocationHelper(ref fb, "email_disable_message_location");
        }

        [TestMethod]
        public void GetAppPropertiesTest()
        {
            var fb = Helpers.GetDesktopContext();

            var appProperties = new String[] {
	                "about_url",
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
	                "wise_mode"
                };
            var result = fb.Admin.GetAppProperties(appProperties);

            Assert.IsFalse(result.IsError, "GetAppProperties failed: {0}", result.IsError ? result.ResponseException.Message : String.Empty);
        }

        [TestMethod]
        public void GetMetricsTest()
        {
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

            var fb = Helpers.GetDesktopContext();
            var result = fb.Admin.GetMetrics(DateTime.Now.AddDays(-7), DateTime.Now, ApplicationMetricPeriod.Day, metrics);

            Assert.IsFalse(result.IsError, "GetMetrics failed: {0}", result.IsError ? result.ResponseException.Message : String.Empty);
        }
    }
}
