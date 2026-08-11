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
    /// Summary description for StatusTests1
    /// </summary>
    [TestClass]
    public class StatusTests
    {
        public StatusTests()
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
        public void GetStatus()
        {
            var context = this.GetContextWithSession();

            var result = context.Status.Get(20);
            Assert.IsFalse(result.IsError);

        }
    }
}
