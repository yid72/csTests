using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.IO;

namespace CsTests.Mail
{
    [TestClass]
    public class Mail
    {
        private const string SmtpServer = "omail.redmond.corp.microsoft.com";
        private const string FromAddress = "yid@microsoft.com";
//        private const string ToAddress = "yid@microsoft.com";
        private const string ToAddress = "yidong.du@yahoo.com";
        private const string Subject = "Yidong Email Test";

        private const string ImageFolder = "CsTests.Mail.Images";
        private readonly string[,] ImageFileInfo = new string[4, 2] {
            {"121", "bird.jpg"},
            {"122", "cat.jpg" },
            {"123", "dog.jpg" },
            {"124", "lion.jpg" }
        };

        private SmtpClient smtpClient;

        [TestInitialize]
        public void Setup()
        {
            smtpClient = new SmtpClient(SmtpServer);
        }

        [TestMethod]
        public void TestMultipart()
        {
            string body = "<html>this is a <b>html</b> body.</html>";
            string alertnateHtml = "<html>this is a <b>html</b> alternate.</html>";

            MailMessage msg = this.CreateMessage();
 
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(alertnateHtml, new ContentType("text/html"));

            msg.AlternateViews.Add(alternate);

            // msg.Body = body;
            msg.Body = "";
            msg.IsBodyHtml = true;

            SendMessage(msg);
        }

        [TestMethod]
        public void TestInlineImages()
        {
            MailMessage msg = this.CreateMessage("Test inline images", "Hello, this is a test for inline images");

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(@"<p>Hello <img src='cid:123'></p>", null, "text/html");
            msg.AlternateViews.Add(htmlView);

            AddInlineImages(htmlView);

            msg.Body = null;
            msg.IsBodyHtml = true;

            SendMessage(msg);
        }

        [TestMethod]
        public void TestAttachments()
        {
            MailMessage msg = this.CreateMessage("Test attachments", "Hello, this is a test for attachments");

            AttachImages(msg);

            this.SendMessage(msg);
        }

        private MailMessage CreateMessage()
        {
            return CreateMessage(Subject, "Hello. This is plain text.");
        }

        private MailMessage CreateMessage(string subject, string body)
        {
            MailMessage msg = new MailMessage(FromAddress, ToAddress, subject, body);
            msg.IsBodyHtml = true;
            return msg;
        }

        private void SendMessage(MailMessage msg)
        {
            smtpClient.Send(msg);
        }

        private void AddInlineImages(AlternateView htmlView)
        {
            for (int i = 0; i < 4; i++)
            {
                //string path = ImageFolder + "." + ImageFileInfo[i, 1];
                //byte[] bytes = File.ReadAllBytes("c:\\temp\\" + ImageFileInfo[i, 1]);
                //                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
//                stream.CopyTo(ms);
//                var ms = new MemoryStream();
//                ms.Write(bytes, 0, bytes.Length);

                LinkedResource linkedResource = new LinkedResource("c:\\temp\\" + ImageFileInfo[i, 1], MediaTypeNames.Image.Jpeg);
                //linkedResource.ContentType = new ContentType("image/jpeg");
                linkedResource.ContentId = ImageFileInfo[i, 0];
                htmlView.LinkedResources.Add(linkedResource);
            }
        }

        private void AttachImages(MailMessage msg)
        {
            for (int i = 0; i < 4; i++)
            {
                msg.Attachments.Add(new Attachment("c:\\temp\\" + ImageFileInfo[i, 1]));
            }
        }
    }
}
