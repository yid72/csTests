using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_Tests.Moq
{
    public class School : ISchool
    {
        Dictionary<string, Student> students;

        public School()
        {
            this.students = new Dictionary<string, Student>();
        }

        public int GetStudentCount()
        {
            return this.students.Count;
        }

        public Student GetStudent(string name)
        {
            return this.students[name];
        }

        public async Task<Student> GetStudentAsync(string name)
        {
            await Task.Delay(1000);
            return GetStudent(name);
        }

        public void AddStudent(Student student)
        {
            this.students[student.Name] = student;
        }

        public async Task AddStudentAsync(Student student)
        {
            await Task.Delay(1000);
            AddStudent(student);
        }
    }
}
