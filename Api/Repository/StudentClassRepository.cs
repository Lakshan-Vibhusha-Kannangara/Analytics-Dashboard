using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class StudentClassRepository : IStudentClassRepository
    {
        private readonly SchoolDBContext _context;

        public StudentClassRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<StudentClass> GetAllStudentClasses()
        {
            return _context.StudentClasses.Include(sc => sc.Student).Include(sc => sc.Class).ToList();
        }

        public StudentClass GetStudentClassById(int studentClassId)
        {
            return _context.StudentClasses.FirstOrDefault(sc => sc.ID == studentClassId);
        }

     public StudentClass AddStudentClass(StudentClass studentClass)
{
    // Check if there is an existing record with the same ID
       var existingStudentClass = _context.StudentClasses.FirstOrDefault(sc =>
        sc.StudentID == studentClass.StudentID &&
        sc.ClassroomID == studentClass.ClassroomID);
    if (existingStudentClass == null)
    {
        // There is no existing record, so add the new StudentClass
        _context.StudentClasses.Add(studentClass);
        _context.SaveChanges();
        return studentClass;
    }
    else
    {
        // A record with the same ID already exists, return the existing one
        return existingStudentClass;
    }
}


        public void UpdateStudentClass(StudentClass studentClass)
        {
            _context.StudentClasses.Update(studentClass);
            _context.SaveChanges();
        }

        public void DeleteStudentClass(int studentClassId)
        {
            var studentClass = GetStudentClassById(studentClassId);
            if (studentClass != null)
            {
                _context.StudentClasses.Remove(studentClass);
                _context.SaveChanges();
            }
        }

     
    }


}
