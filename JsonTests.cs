using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cs_Tests.Models;
using System.Runtime.Serialization.Json;
using System.IO;

namespace cs_Tests
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
            Console.WriteLine(reader.ReadToEnd());
        }
    }
}
