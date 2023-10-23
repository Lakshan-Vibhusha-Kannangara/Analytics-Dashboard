

  using System;
using System.Collections.Generic;


namespace API.Repository
{
    public interface ISchoolClassRepository
    {
        IEnumerable<SchoolClass> GetAllSchoolClasses();
        SchoolClass GetSchoolClassById(int schoolClassId);
        SchoolClass AddSchoolClass(SchoolClass schoolClass);
        void UpdateSchoolClass(SchoolClass schoolClass);
        void DeleteSchoolClass(int schoolClassId);
        int CountSchoolClassesByCriteria(int schoolId, int classId);
    }
}

