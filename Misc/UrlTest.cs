using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Collections.Specialized;

namespace cs_Tests.Misc
{
    [TestClass]
    public class UrlTest
    {
        [TestMethod]
        public void TestGetLeftPart()
        {
            var uri = new Uri("https://microsoft-my.sharepoint.com/personal/15/_layouts/UnsubscribeAlert.aspx?email=tester@microsoft.com&key=1");
            string path = uri.GetLeftPart(UriPartial.Path);
            Console.WriteLine(path);
        }

        [TestMethod]
        public void TestQueryParameters()
        {
            NameValueCollection p = HttpUtility.ParseQueryString(String.Empty);
            p["foo"] = "hello";
            p["bar"] = "world  good";
            Console.WriteLine(p.ToString());
        }
    }
}
