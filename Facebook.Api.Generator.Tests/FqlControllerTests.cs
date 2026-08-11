using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facebook.Api.Generator.Tests
{
    /// <summary>
    /// Summary description for FqlControllerTests
    /// </summary>
    [TestClass]
    public class FqlControllerTests
    {
        public FqlControllerTests()
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
        public void QueryUsers()
        {
            var fb = Helpers.GetDesktopContext();
            var result = fb.Fql.Query<User>("SELECT first_name,last_name,uid FROM user WHERE uid in (601599551,160000629)");

            Assert.IsFalse(result.IsError, "Query failed: {0}", result.IsError ? result.ResponseException.Message : String.Empty);
            Assert.IsTrue(result.Value.Count == 2);
            Assert.AreEqual("Daniel", result.Value[0].FirstName);
        }

        [TestMethod]
        public void QueryXml()
        {
            var fb = Helpers.GetDesktopContext();

            var result = fb.Fql.QueryXml("SELECT first_name,last_name,uid FROM user WHERE uid = 601599551");

            Assert.IsFalse(result.IsError, "Query failed: {0}", result.IsError ? result.ResponseException.Message : String.Empty);
            Assert.AreEqual("Daniel", result.Value.Descendants("first_name").First().Value);
        }

        [TestMethod]
        public void QueryJson()
        {
            var fb = Helpers.GetDesktopContext();

            var result = fb.Fql.QueryJson("SELECT first_name,last_name,uid FROM user WHERE uid = 601599551");
            Assert.IsFalse(result.IsError, "Query failed: {0}", result.IsError ? result.ResponseException.Message : String.Empty);            
        }

        /*[TestMethod]
        public void Multiquery()
        {
            var fb = Helpers.GetDesktopContext();
            using (var mq = FqlMultiquery.Start(fb))
            {
                mq.Add<User>("Daniel", "SELECT first_name,last_name,uid FROM user WHERE uid = 601599551");
                mq.Add<User>("Irina", "SELECT first_name,last_name,uid FROM user WHERE uid = 160000629");                
            }

            var mqResponse = fb.Fql.Multiquery();
            var daniel = mqResponse.Value.GetResult<User>("Daniel");
            var irina = mqResponse.Value.GetResult<User>("Irina");

            Assert.AreEqual("Daniel", daniel.FirstName);
            Assert.AreEqual("Irina", irina.FirstName);
        }*/
    }
}
