using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Collections.Specialized;

namespace cs_Tests.Misc
{
    [TestClass]
    public class UrlTest
    {
        private const string TesterEmail = "tester@microsoft.com";
        private const string HelloWorld = "hello world";
        private const string HelloWorld1 = "hello+world";
        private string testBaseUrl = "http://www.foo.com/foo.aspx";

        [TestMethod]
        public void TestUrlBuilder()
        {
            var uriBuilder = new UriBuilder(this.testBaseUrl);
            Console.WriteLine(uriBuilder.ToString());
            Console.WriteLine(uriBuilder.Query.ToString());
        }

        [TestMethod]
        public void TestEscapeDataString()
        {
            Console.WriteLine(HttpUtility.UrlEncode(HelloWorld));
            Console.WriteLine(Uri.EscapeUriString(HelloWorld));
            Console.WriteLine(Uri.EscapeDataString(HelloWorld));
        }

        [TestMethod]
        public void TestGetLeftPart()
        {
            var uri = new Uri("https://microsoft-my.sharepoint.com/personal/15/_layouts/UnsubscribeAlert.aspx?email=tester@microsoft.com&key=1");
            string path = uri.GetLeftPart(UriPartial.Path);
            Console.WriteLine(path);
        }

        [TestMethod]
        public void TestBuildQuery()
        {
            NameValueCollection p = HttpUtility.ParseQueryString(String.Empty);
            p["email"] = TesterEmail;
            p["value"] = HelloWorld;

            Assert.AreEqual("email=tester%40microsoft.com&value=hello+world", p.ToString());
        }

        [TestMethod]
        public void TestBuildQuery1()
        {
            NameValueCollection p = HttpUtility.ParseQueryString(String.Empty);
            p["email"] = TesterEmail;
            p["value"] = HelloWorld1;

            Console.WriteLine(p.ToString());
            Assert.AreEqual("email=tester%40microsoft.com&value=hello%2bworld", p.ToString());
        }

        [TestMethod]
        public void TestParseQuery()
        {
            NameValueCollection p = HttpUtility.ParseQueryString("email=tester%40microsoft.com&value=hello+world");
            Assert.AreEqual(TesterEmail, p["email"]);
            Assert.AreEqual(HelloWorld, p["value"]);
        }

        [TestMethod]
        public void TestParseQuery1()
        {
            NameValueCollection p = HttpUtility.ParseQueryString("email=tester%40microsoft.com&value=hello%2bworld");
            Assert.AreEqual(TesterEmail, p["email"]);
            Assert.AreEqual(HelloWorld1, p["value"]);
        }


        [TestMethod]
        public void TestParseQueryParameters2()
        {
            NameValueCollection p = HttpUtility.ParseQueryString(String.Empty);
            p["email"] = Uri.EscapeDataString("admin@prepspo.msolctp-int.com");
            p["key"] = Uri.EscapeDataString("let's say hello+world");

            string escaped = p.ToString();
            Console.WriteLine("Escaped url: " + escaped);

            string unescaped = HttpUtility.UrlDecode(escaped);

            NameValueCollection p1 = HttpUtility.ParseQueryString(unescaped);
            Console.WriteLine("email: " + p1["email"]);
            Console.WriteLine("key: " + p1["key"]);
        }

        public void TestQueryParametersBad()
        {
            NameValueCollection p2 = HttpUtility.ParseQueryString("email=admin@prepspo.msolctp-int.com&key=hello+world");
            Console.WriteLine("email: " + p2["email"]);
            Console.WriteLine("key: " + p2["key"]);
        }
    }
}
