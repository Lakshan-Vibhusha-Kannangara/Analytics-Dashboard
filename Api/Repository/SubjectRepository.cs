using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolDBContext _context;

        public SubjectRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            return _context.Subjects.ToList();
        }

        public Subject GetSubjectById(int subjectId)
        {
            try
            {
                return _context.Subjects.FirstOrDefault(subject => subject.SubjectID == subjectId);
            }
            catch
            {
                return null;
            }
        }

public Subject AddSubject(Subject subject)
{
    // Check if there are no existing subjects with the same name
    var existingSubject = _context.Subjects.FirstOrDefault(s => s.SubjectName == subject.SubjectName);

    if (existingSubject == null)
    {
        _context.Subjects.Add(subject);
        _context.SaveChanges();
        return _context.Subjects.FirstOrDefault(s => s.SubjectName == subject.SubjectName);

    }
    else
    {
      return existingSubject;
    }


}

        public void UpdateSubject(Subject subject)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = GetSubjectById(subjectId);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
            }
        }

        public int CountSubjectsByCriteria(string subjectName)
        {
            return _context.Subjects.Count(subject => subject.SubjectName == subjectName);
        }

   
    }
}
