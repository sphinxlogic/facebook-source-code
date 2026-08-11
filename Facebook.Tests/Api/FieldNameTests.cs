using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Api;

namespace Facebook.Tests.Api
{
    /// <summary>
    /// Summary description for UserPropertyTests
    /// </summary>
    [TestClass]
    public class FieldNameTests
    {
        public FieldNameTests()
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
        public void GetPropertyNames()
        {
            var propertySet = UserProperty.Uid | UserProperty.FirstName | UserProperty.LastName | UserProperty.Movies | UserProperty.PicBig | UserProperty.WorkHistory;
            var result = propertySet.ToFieldNames();

            Assert.AreEqual(6, result.Length);
            Assert.IsTrue(result.Contains("uid"), "Missing uid");
            Assert.IsTrue(result.Contains("first_name"), "Missing first_name");
            Assert.IsTrue(result.Contains("last_name"), "Missing last_name");
            Assert.IsTrue(result.Contains("movies"), "Missing movies");
            Assert.IsTrue(result.Contains("pic_big"), "Missing pic_big");
            Assert.IsTrue(result.Contains("work_history"), "Missing work_history");
        }

        [TestMethod]
        public void FieldsToValues()
        {
            var fields = "uid,first_name,last_name";
            var expected = UserProperty.Uid | UserProperty.FirstName | UserProperty.LastName;
            var actual = FieldName.FieldsToValues<UserProperty>(fields);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMissing()
        {
            var required = UserProperty.Uid | UserProperty.FirstName | UserProperty.LastName;
            var has = UserProperty.Uid;
            var expected = UserProperty.FirstName | UserProperty.LastName;
            var missing = has ^ required;

            Assert.AreEqual(expected, missing);
        }
    }
}