using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facebook.Api.Generator.Tests
{
    /// <summary>
    /// Summary description for FriendsControllerTests
    /// </summary>
    [TestClass]
    public class FriendsControllerTests
    {
        public FriendsControllerTests()
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
        public void Get()
        {
            var fb = Helpers.GetDesktopContext();

            var friendsResult = fb.Friends.Get(601599551);

            Assert.IsFalse(friendsResult.IsError, "Friends.Get() returned an error: {0}", friendsResult.IsError ? friendsResult.ResponseException.Message : String.Empty);
            Assert.IsTrue(friendsResult.Value.Count > 0, "Count is zero");
        }

        [TestMethod]
        public void GetNoUid()
        {
            var fb = Helpers.GetDesktopContext();

            var friendsResult = fb.Friends.Get();

            Assert.IsFalse(friendsResult.IsError, "Friends.Get() returned an error: {0}", friendsResult.IsError ? friendsResult.ResponseException.Message : String.Empty);
            Assert.IsTrue(friendsResult.Value.Count > 0, "Count is zero");
        }

        [TestMethod]
        public void GetAuthorized()
        {
            var fb = Helpers.GetDesktopContext();
            Helpers.EstablishSession(ref fb);

            var friendsResult = fb.Friends.Get(601599551);

            Assert.IsFalse(friendsResult.IsError, "Friends.Get() returned an error: {0}", friendsResult.IsError ? friendsResult.ResponseException.Message : String.Empty);
            Assert.IsTrue(friendsResult.Value.Count > 0, "Count is zero");
        }

        [TestMethod]
        public void GetAuthorizedNoUid()
        {
            var fb = Helpers.GetDesktopContext();
            Helpers.EstablishSession(ref fb);

            var friendsResult = fb.Friends.Get();

            Assert.IsFalse(friendsResult.IsError, "Friends.Get() returned an error: {0}", friendsResult.IsError ? friendsResult.ResponseException.Message : String.Empty);
            Assert.IsTrue(friendsResult.Value.Count > 0, "Count is zero");
        }
    }
}
