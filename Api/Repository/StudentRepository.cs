using API.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDBContext _context;

        public StudentRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student GetStudentById(int studentId)
        {
            try
            {
                return _context.Students.FirstOrDefault(student => student.StudentID == studentId);
            }
            catch
            {
                return null;
            }
        }

      public Student AddStudent(Student student)
{
    // Check if there are no existing students with the same criteria
    var existingStudent = _context.Students.FirstOrDefault(s =>
        s.FirstName == student.FirstName &&
        s.LastName == student.LastName &&
        s.YearLevel == student.YearLevel);

    if (existingStudent == null)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
     return  _context.Students.FirstOrDefault(s =>
        s.FirstName == student.FirstName &&
        s.LastName == student.LastName &&
        s.YearLevel == student.YearLevel);

    }
    else
    {
       return existingStudent;
    }
}


        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(int studentId)
        {
            var student = GetStudentById(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public int CountStudentsByCriteria(string firstName, string lastName, int yearLevel)
        {
            string sqlQuery = "SELECT StudentID, FirstName, LastName, YearLevel FROM Students " +
                "WHERE (@FirstName IS NULL OR FirstName = @FirstName) " +
                "AND (@LastName IS NULL OR LastName = @LastName) " +
                "AND (@YearLevel = 0 OR YearLevel = @YearLevel)";

            var parameters = new[]
            {
                new MySqlConnector.MySqlParameter("@FirstName", firstName),
                new MySqlConnector.MySqlParameter("@LastName", lastName),
                new MySqlConnector.MySqlParameter("@YearLevel", yearLevel)
            };

            var result = _context.Students
                .FromSqlRaw(sqlQuery, parameters)
                .ToList();

            return result.Count;
        }

      
    }
}
