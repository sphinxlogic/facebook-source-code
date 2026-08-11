using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Offline;
using Facebook.Api;

namespace Facebook.Tests.Api.Controllers
{
    /// <summary>
    /// Summary description for DashboardTests
    /// </summary>
    [TestClass]
    public class DashboardTests
    {
        public DashboardTests()
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

        private FacebookOfflineAccessContext GetContext()
        {
            return new FacebookOfflineAccessContext(ApplicationType.Website, "f5c35879709928e3b3a7076af6cee741", "8b9df20d9b10f10320aa00d5f5fbad56");
        }

        private FacebookOfflineAccessContext GetContextWithSession()
        {
            var context = this.GetContext();

            context.Session = new SessionInfo
            {
                SessionKey = "1275ac5854585a81ac268082-601599551",
                Uid = 601599551,
                ApiKey = "f5c35879709928e3b3a7076af6cee741",
                Expires = DateTime.Parse("9999-12-31 23:59:59.997"),
                Secret = "0039f3bf15b5f03fd76c2445247be149"
            };

            return context;
        }

        [TestMethod]
        public void MultiIncrement()
        {
            var context = GetContext();

            /*var result = context.Dashboard.MultiIncrementCount(601599551, 601599551, 601599551, 601599551, 601599551, 601599551);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else Assert.IsFalse(result.Value.Any(v => !v));*/
        }

        private Int64 AddNewsHelper(out FacebookResponse<Int64> result)
        {
            var news = new DashboardItem
            {
                Message = "User Foo!",
                ActionLink = new DashboardActionLink { Href = "http://dandoes.net", Text = "Bar!" }
            };

            var context = GetContext();
            result = context.Dashboard.AddNews(601599551, news);
            return result.Value;
        }

        [TestMethod]
        public void AddNews()
        {
            FacebookResponse<Int64> result;
            this.AddNewsHelper(out result);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else Assert.IsFalse(result.Value == 0);

        }

        private Int64 AddGlobalNewsHelper(out FacebookResponse<Int64> result)
        {
            var news = new DashboardItem
            {
                Message = "Global Foo!",
                ActionLink = new DashboardActionLink { Href = "http://dandoes.net", Text = "Bar!" }
            };

            var context = GetContext();
            result = context.Dashboard.AddGlobalNews(news);
            return result.Value;
        }

        [TestMethod]
        public void AddGlobalNews()
        {
            FacebookResponse<Int64> result;
            this.AddGlobalNewsHelper(out result);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else Assert.IsFalse(result.Value == 0);
        }

        [TestMethod]
        public void ClearNews()
        {
            var context = GetContext();
            var result = context.Dashboard.ClearNews(601599551);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);

        }

        [TestMethod]
        public void ClearGlobalNews()
        {
            var context = GetContext();
            var result = context.Dashboard.ClearGlobalNews();
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
        }

        [TestMethod]
        public void GetNews()
        {
            this.ClearNews();

            var context = this.GetContext();
            FacebookResponse<Int64> addNewsResult;
            var newsId = this.AddNewsHelper(out addNewsResult);
            Assert.IsTrue(newsId > 0, "Adding news failed");
            var result = context.Dashboard.GetNews(601599551);

            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value.Count > 0, "No news items returned");
                Assert.AreEqual(newsId, result.Value.First().Id);
                Assert.AreEqual("User Foo!", result.Value.First().News.First().Message);
            }
        }

        [TestMethod]
        public void GetGlobalNews()
        {
            this.ClearGlobalNews();

            var context = this.GetContext();
            FacebookResponse<Int64> addNewsResult;
            var newsId = this.AddGlobalNewsHelper(out addNewsResult);
            Assert.IsTrue(newsId > 0, "Adding news failed");
            var result = context.Dashboard.GetGlobalNews();

            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value.Count > 0, "No news items returned");
                Assert.AreEqual(newsId, result.Value.First().Id);
                Assert.AreEqual("Global Foo!", result.Value.First().News.First().Message);
            }
        }

        private FacebookResponse<FacebookAttributeBasedList<MultiAddNewsResult>> MultiAddNewsHelper()
        {
            var news = new DashboardItem
            {
                Message = "User Foo!",
                ActionLink = new DashboardActionLink { Href = "http://dandoes.net", Text = "Bar!" }
            };

            var context = GetContext();
            return context.Dashboard.MultiAddNews(new DashboardItem[] { news }, 601599551, 160000629);
        }

        [TestMethod]
        public void MultiAddNews()
        {
            var result = MultiAddNewsHelper();

            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(2, result.Value.Count);
                Assert.IsTrue(result.Value[0].WasAdded);
                Assert.IsTrue(result.Value[1].WasAdded);
            }
        }

        [TestMethod]
        public void MultiClearNews()
        {
            this.MultiAddNewsHelper();

            var context = this.GetContext();
            var result = context.Dashboard.MultiClearNews(new MultiNewsRequestItem { Uid = 601599551 }, new MultiNewsRequestItem { Uid = 160000629 });

            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(2, result.Value.Count);
                Assert.IsTrue(result.Value[0].WasCleared);
                Assert.IsTrue(result.Value[1].WasCleared);
            }
        }

        [TestMethod]
        public void MultiGetNews()
        {
            var addNewsResult = this.MultiAddNewsHelper();

            var context = this.GetContext();
            var result = context.Dashboard.MultiGetNews(new MultiNewsRequestItem { Uid = 601599551 }, new MultiNewsRequestItem { Uid = 160000629 });
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(2, result.Value.Count);
                Assert.AreEqual(addNewsResult.Value[0].NewsId, result.Value[0].News[0].Id);
                Assert.AreEqual(addNewsResult.Value[1].NewsId, result.Value[1].News[0].Id);
            }
        }

        private FacebookResponse<Int64> PublishActivityHelper()
        {
            var context = this.GetContextWithSession();
            var activity = new DashboardActivityPublishItem
            {
                Message = "{*actor*} ate some really awesome cookies! Take that, @:160000629!",
                ActionLink = new DashboardActionLink { Href = "http://dandoes.net", Text = "Cookies are good!" }
            };

            return context.Dashboard.PublishActivity(activity);
        }

        [TestMethod]
        public void PublishActivity()
        {
            var result = this.PublishActivityHelper();

            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value > 0);
            }
        }

        [TestMethod]
        public void GetActivity()
        {
            var activityResult = this.PublishActivityHelper();

            var context = this.GetContextWithSession();

            var result = context.Dashboard.GetActivity();
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value.Count > 0);
                Assert.AreEqual(activityResult.Value, result.Value[0].Id);
            }
        }

        [TestMethod]
        public void RemoveActivity()
        {
            var activityResult = this.PublishActivityHelper();

            var context = this.GetContextWithSession();

            var result = context.Dashboard.RemoveActivity(activityResult.Value);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value.Count > 0);
                Assert.IsTrue(result.Value[0].WasRemoved);
            }
        }

        [TestMethod]
        public void IncrementCount()
        {
            var context = this.GetContext();

            var result = context.Dashboard.IncrementCount(601599551);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value);
            }
        }

        [TestMethod]
        public void DecrementCount()
        {
            this.IncrementCount();

            var context = this.GetContext();

            var result = context.Dashboard.DecrementCount(601599551);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value);
            }
        }

        [TestMethod]
        public void SetCount()
        {
            var context = this.GetContext();

            var result = context.Dashboard.SetCount(601599551, 5);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.IsTrue(result.Value);
            }
        }

        [TestMethod]
        public void GetCount()
        {
            this.SetCount();

            var context = this.GetContext();

            var result = context.Dashboard.GetCount(601599551);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(5, result.Value);
            }
        }

        [TestMethod]
        public void MultiIncrementCount()
        {
            var context = this.GetContext();

            var result = context.Dashboard.MultiIncrementCount(601599551, 160000629);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(2, result.Value.Count);
                Assert.IsTrue(result.Value[0].Success);
                Assert.IsTrue(result.Value[0].Success);
            }
        }

        [TestMethod]
        public void MultiDecrementCount()
        {
            this.MultiIncrementCount();
            var context = this.GetContext();

            var result = context.Dashboard.MultiDecrementCount(601599551, 160000629);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(2, result.Value.Count);
                Assert.IsTrue(result.Value[0].Success);
                Assert.IsTrue(result.Value[0].Success);
            }
        }

        [TestMethod]
        public void MultiSetCount()
        {
            var context = this.GetContext();

            var result = context.Dashboard.MultiSetCount(
                new DashboardMultiSetCountItem { Uid = 601599551, Count = 3 },
                new DashboardMultiSetCountItem { Uid = 160000629, Count = 5 });
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(2, result.Value.Count);
                Assert.IsTrue(result.Value[0].Success);
                Assert.IsTrue(result.Value[0].Success);
            }
        }

        [TestMethod]
        public void MultiGetCount()
        {
            this.MultiSetCount();

            var context = this.GetContext();

            var result = context.Dashboard.MultiGetCount(601599551, 160000629);
            if (result.IsError) Assert.Fail(result.ResponseException.Message);
            else
            {
                Assert.AreEqual(2, result.Value.Count);
                Assert.AreEqual(3, result.Value[0].Count);
                Assert.AreEqual(5, result.Value[1].Count);
            }
        }
    }
}