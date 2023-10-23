using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface ISubjectClassRepository
    {
        IEnumerable<SubjectClass> GetAllSubjectClasses();
        SubjectClass GetSubjectClassById(int subjectClassId);
        SubjectClass AddSubjectClass(SubjectClass subjectClass);
        void UpdateSubjectClass(SubjectClass subjectClass);
        void DeleteSubjectClass(int subjectClassId);
    }

    public class SubjectClassRepository : ISubjectClassRepository
    {
        private readonly SchoolDBContext _context;

        public SubjectClassRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<SubjectClass> GetAllSubjectClasses()
        {
            return _context.SubjectClasses.ToList();
        }

        public SubjectClass GetSubjectClassById(int subjectClassId)
        {
            try
            {
                return _context.SubjectClasses.FirstOrDefault(subjectClass => subjectClass.SubjectClassID == subjectClassId);
            }
            catch
            {
                return null;
            }
        }

public SubjectClass AddSubjectClass(SubjectClass subjectClass)
{
    var existingSubjectClass = _context.SubjectClasses
        .FirstOrDefault(sc => sc.SubjectID == subjectClass.SubjectID && sc.ClassID == subjectClass.ClassID);

    if (existingSubjectClass == null)
    {
        _context.SubjectClasses.Add(subjectClass);
        _context.SaveChanges(); // Save changes to the database

        // Retrieve the newly added SubjectClass from the database
        return _context.SubjectClasses.FirstOrDefault(sc => sc.SubjectID == subjectClass.SubjectID && sc.ClassID == subjectClass.ClassID);
    }
    else
    {
        return existingSubjectClass; // Return the existing SubjectClass
    }
}


        public void UpdateSubjectClass(SubjectClass subjectClass)
        {
            _context.SubjectClasses.Update(subjectClass);
            _context.SaveChanges();
        }

        public void DeleteSubjectClass(int subjectClassId)
        {
            var subjectClass = GetSubjectClassById(subjectClassId);
            if (subjectClass != null)
            {
                _context.SubjectClasses.Remove(subjectClass);
                _context.SaveChanges();
            }
        }

     
    }
}
