using System;
using System.Collections.Generic;
using API.Models;

namespace API.Repository
{
    public interface IStudentClassRepository
    {
        IEnumerable<StudentClass> GetAllStudentClasses();
        StudentClass GetStudentClassById(int studentClassId);
        StudentClass AddStudentClass(StudentClass studentClass);
        void UpdateStudentClass(StudentClass studentClass);
        void DeleteStudentClass(int studentClassId);
    }
}
