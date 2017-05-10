using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_Tests.Moq
{
    [TestClass]
    public class MoqTest
    {
        [TestMethod]
        public void TestMock()
        {
            Student student = new Student("Mike");
            var mockCount = 1;

            var mock = new Mock<ISchool>();
            mock.Setup(s => s.GetStudentCount()).Returns(mockCount);
            mock.Setup(s => s.GetStudent(It.IsAny<string>())).Returns(student);

            Assert.AreEqual(1, mock.Object.GetStudentCount());
            Assert.AreEqual(student.Name, mock.Object.GetStudent("any name").Name);
        }
    }
}
