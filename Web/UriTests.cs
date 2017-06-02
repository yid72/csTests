using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Collections.Specialized;

namespace csTests.Web
{
    [TestClass]
    public class UriTest
    {
        private const string TesterEmail = "tester@microsoft.com";
        private const string HelloWorld = "hello world+/%";
        private const string HelloWorld1 = "hello+world";
        private const string TextWithSpace = "hello ";
        private string testBaseUrl = "http://www.foo.com/path/foo.aspx";
        private string SpecialUrl = "http://www.foo.com/path/hello world%#.docx?a=b";
        private string EscapedUriSpecialUrl = "http://www.foo.com/path/hello%20world%25#.docx?a=b";
        private string EscapedDataSpecialUrl = "http%3A%2F%2Fwww.foo.com%2Fpath%2Fhello%20world%25%23.docx%3Fa%3Db";
        private Uri myUri;

        [TestInitialize]
        public void TestInitialize()
        {
            this.myUri = new Uri(this.testBaseUrl);   
        }

        [TestMethod]
        public void TestUriBuilder()
        {
            var uriBuilder = new UriBuilder(this.testBaseUrl);
            Console.WriteLine(uriBuilder.ToString());
            Console.WriteLine(uriBuilder.Query.ToString());
        }

        [TestMethod]
        public void TestEscapeUriString()
        {
            // RFC2396 reserved characters ";/?:@&=+$," are not encoded.
            Assert.AreEqual(EscapedUriSpecialUrl, Uri.EscapeUriString(SpecialUrl));

            Assert.AreEqual("hello%20", Uri.EscapeUriString(TextWithSpace));
        }

        [TestMethod]
        public void TestEscapeDataString()
        {
            Assert.AreEqual(EscapedDataSpecialUrl, Uri.EscapeDataString(SpecialUrl));

            // NOTE: EscapeDataString() doesn't convert a space to '+'!
            Assert.AreEqual("hello%20", Uri.EscapeDataString(TextWithSpace));
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
            p["value1"] = HttpUtility.UrlEncode(HelloWorld);

            Console.WriteLine("p = " + p.ToString());

            var builder = new UriBuilder();
            builder.Query = p.ToString();
            Console.WriteLine(builder.ToString());

            builder.Query = "value=" + Uri.EscapeDataString("hello world");
            Console.WriteLine(builder.ToString());
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

        [TestMethod]
        public void TestUriQueryParametersBad()
        {
            NameValueCollection p2 = HttpUtility.ParseQueryString("email=admin@prepspo.msolctp-int.com&key=hello+world");
            Console.WriteLine("email: " + p2["email"]);
            Console.WriteLine("key: " + p2["key"]);
        }

        [TestMethod]
        public void TestUriGetComponents()
        {
            string str = this.myUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
            Assert.AreEqual("http://www.foo.com", str);
        }

        [TestMethod]
        public void TestUriSegments()
        {
            string[] segs = this.myUri.Segments;
            Assert.IsNotNull(segs);
            Assert.AreEqual(3, segs.Length);
            Assert.AreEqual("/", segs[0]);
            Assert.AreEqual("path/", segs[1]);
            Assert.AreEqual("foo.aspx", segs[2]);
        }
    }
}
