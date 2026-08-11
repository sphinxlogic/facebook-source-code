using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facebook.Api.Generator.Tests
{
    /// <summary>
    /// Summary description for UserTests
    /// </summary>
    [TestClass]
    public class UserTests
    {
        public UserTests()
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
        public void BirthdayDatePartial()
        {
            var input = "11/3";
            var expected = new DateTime(DateTime.Now.Year, 11, 3);
            var actual = new User(new Dictionary<String, Object> { { "birthday_date", input } }).BirthdayDate;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BirthdayDateFull()
        {
            var input = "11/3/1982";
            var expected = new DateTime(1982, 11, 3);
            var actual = new User(new Dictionary<String, Object> { { "birthday_date", input } }).BirthdayDate;

            Assert.AreEqual(expected, actual);
        }
    }
}
