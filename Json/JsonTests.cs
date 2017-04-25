using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsTests.Json.Models;
using System.Runtime.Serialization.Json;
using System.IO;

namespace CsTests.Json
{
    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void TestOutput()
        {
            var model = new HelloWorldModel();
            model.Hello = "Hello World";
            var ser = new DataContractJsonSerializer(typeof(HelloWorldModel));
            var stream = new MemoryStream();
            ser.WriteObject(stream, model);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            Console.WriteLine("Json format: " + reader.ReadToEnd());
        }
    }
}
