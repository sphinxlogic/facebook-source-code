using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facebook.Api.Generator.Tests
{
    /// <summary>
    /// Summary description for UnixDateTimeTests
    /// </summary>
    [TestClass]
    public class UnixDateTimeTests
    {
        public UnixDateTimeTests()
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
        public void ConvertBothWays()
        {
            DateTime original = DateTime.Now;
            UnixDateTime unix = original.ToUnixDateTime();
            DateTime originalConverted = unix.ToDateTime();
            UnixDateTime unixConverted = originalConverted.ToUnixDateTime();

            Int64 originalMargin = Math.Abs(original.Ticks - originalConverted.Ticks);
            Double unixMargin = Math.Abs(unix.Value - unixConverted.Value);

            // Converting to and from will always lose precision, so just make sure it's within an acceptable range.
            Assert.IsTrue(originalMargin < 1000, "Conversion from unix time failed");
            Assert.IsTrue(unixMargin < 1, "Convert back to unix time failed");
        }
    }
}
