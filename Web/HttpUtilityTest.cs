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
        public void TestUrlEncode()
        {
            Console.WriteLine(HttpUtility.UrlEncode(SpecialPath));
        }

        [TestMethod]
        public void TestHtmlEncode()
        {
            Assert.AreEqual("&quot;&lt;&gt;&amp;", HttpUtility.HtmlEncode(@"""<>&"));
            Console.WriteLine(HttpUtility.HtmlEncode(Hello));
        }
    }
}
