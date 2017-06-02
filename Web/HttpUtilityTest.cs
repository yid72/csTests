using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cs_Tests.Web
{
    [TestClass]
    public class HttpUtilityTest
    {
        private const string Hello = @"hello world""'%<+/";
        private const string SpecialPath = "http://www.foo.com/hello world%#.txt";

        [TestMethod]
        public void TestHttpUtilityUrlEncode()
        {
            // All researved characters are escaped. The space is converted to +
            Assert.AreEqual("http%3a%2f%2fwww.foo.com%2fhello+world%25%23.txt", HttpUtility.UrlEncode(SpecialPath));
        }

        [TestMethod]
        public void TestHttpUtilityUrlPathEncode()
        {
            // The URI reserved characters are kept as is. The space is converted to %20
            Assert.AreEqual("http://www.foo.com/hello%20world%#.txt", HttpUtility.UrlPathEncode(SpecialPath));
        }

        [TestMethod]
        public void TestHttpUtilityHtmlEncode()
        {
            Assert.AreEqual("&quot;&lt;&gt;&amp;", HttpUtility.HtmlEncode(@"""<>&"));
            Console.WriteLine(HttpUtility.HtmlEncode(Hello));
        }
    }
}
