using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface ISchoolRepository
    {
        IEnumerable<School> GetAllSchools();
        School GetSchoolById(int schoolId);
        School AddSchool(School school);
        void UpdateSchool(School school);
        void DeleteSchool(int schoolId);
        int CountSchoolsByCriteria(string schoolName);
    }
}
