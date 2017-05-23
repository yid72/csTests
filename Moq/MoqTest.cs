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
        public async Task TestMock()
        {
            Student student = new Student("Mike");
            var mockCount = 1;

            var mock = new Mock<ISchool>();
            mock.Setup(s => s.GetStudentCount()).Returns(mockCount);
            mock.Setup(s => s.GetStudent(It.IsAny<string>())).Returns(student);
            mock.Setup(s => s.GetStudentAsync(It.IsAny<string>())).ReturnsAsync(student);
            mock.Setup(s => s.AddStudent(It.IsAny<Student>()));
            mock.Setup(s => s.AddStudentAsync(It.IsAny<Student>()));

            Assert.AreEqual(1, mock.Object.GetStudentCount());
            Assert.AreEqual(student.Name, mock.Object.GetStudent("any name").Name);
            Assert.AreEqual(student.Name, mock.Object.GetStudentAsync("name").Result.Name);
            await mock.Object.AddStudentAsync(student);
        }
    }
}
