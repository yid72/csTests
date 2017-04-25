using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CsTests.Json.Models
{
    [DataContract]
    class HelloWorldModel
    {
        private string hello = "Hello";

        [DataMember]
        public string Hello
        {
            get { return hello; }

            set { hello = value; }
        }
    }
}
