﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_Tests.Moq
{
    public interface ISchool
    {
        int GetStudentCount();

        Student GetStudent(string name);
    }
}
