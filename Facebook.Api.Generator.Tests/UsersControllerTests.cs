using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Desktop;
using System.Diagnostics;
using System.Threading;

namespace Facebook.Api.Generator.Tests
{
    /// <summary>
    /// Summary description for UsersContextTests
    /// </summary>
    [TestClass]
    public class UsersControllerTests
    {
        public UsersControllerTests()
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

        [TestMethod]
        public void GetInfoTestNoSession()
        {
            var context = Helpers.GetDesktopContext();
            var userIds = new String[] { "601599551", "1386564816" };
            var properties = UserProperty.FirstName | UserProperty.LastName;

            FacebookList<User> users = context.Users.GetInfo(properties, userIds);
            Assert.AreEqual(userIds.Length, ((IList)users).Count);
            Assert.AreEqual("Daniel", users[0].FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(FacebookApiException))]
        public void ThrowsFacebookApiException()
        {
            var context = Helpers.GetDesktopContext();
            var userIds = new String[] { "601599551a", "1386564816a" };
            var properties = UserProperty.FirstName | UserProperty.LastName;

            var users = context.Users.GetInfo(properties, userIds);
        }

        [TestMethod]
        public void TestBatch()
        {
            var context = Helpers.GetDesktopContext();
            var userIds = new String[] { "601599551", "1386564816" };
            var properties = UserProperty.FirstName | UserProperty.LastName;
             
            FacebookList<User> users = null;
            using (var batch = Batch.Start(context))
            {
                var usersResponse = context.Users.GetInfo(properties, userIds);
                var getAllocationResponse = context.Admin.GetAllocation("notifications_per_day");

                var response = batch.CompleteDebug();

                users = usersResponse;
            }

            Assert.AreEqual(userIds.Length, users.Count);
            Assert.AreEqual("Daniel", users[0].FirstName);
        }

        [TestMethod]
        public void CanParseSimpleReturnValue()
        {
            var context = Helpers.GetDesktopContext();
            Int64 result = context.Admin.GetAllocation("notifications_per_day");
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ParseBoolean()
        {
            Boolean result;
            Boolean.TryParse("1", out result);
            Assert.IsTrue(result);
        }
    }
}
