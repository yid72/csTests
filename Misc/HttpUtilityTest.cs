using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cs_Tests.Misc
{
    [TestClass]
    public class HttpUtilityTest
    {
        private const string Hello = @"hello world""'%<+/";

        [TestMethod]
        public void TestHtmlEncode()
        {
            Assert.AreEqual("&quot;&lt;&gt;&amp;", HttpUtility.HtmlEncode(@"""<>&"));
            Console.WriteLine(HttpUtility.HtmlEncode(Hello));
        }
    }
}
