using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetAllSubjects();
        Subject GetSubjectById(int subjectId);
        Subject AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);
    }
}
