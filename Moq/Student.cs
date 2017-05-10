using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_Tests.Moq
{
    public class Student
    {
        private string name;

        public Student(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public int Grade { get; set; }

        public override string ToString()
        {
            return this.name + ": Grade " + this.Grade;
        }
    }
}
