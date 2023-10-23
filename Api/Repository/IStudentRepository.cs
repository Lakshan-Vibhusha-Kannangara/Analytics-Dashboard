using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int studentId);
        Student AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        int CountStudentsByCriteria(string firstName, string lastName, int yearLevel);
    }
}
