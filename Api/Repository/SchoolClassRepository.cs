using API.Models;
using API.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly SchoolDBContext _context;

        public SchoolClassRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<SchoolClass> GetAllSchoolClasses()
        {
            return _context.SchoolClasses.Include(sc => sc.School).Include(sc => sc.Class).ToList();
        }

        public SchoolClass GetSchoolClassById(int schoolClassId)
        {
            return _context.SchoolClasses.FirstOrDefault(sc => sc.ID == schoolClassId);
        }

     public SchoolClass AddSchoolClass(SchoolClass schoolClass)
{
    // Check if there is an existing record with the same ClassName
   var existingSchoolClass = _context.SchoolClasses.FirstOrDefault(sc =>
        sc.ClassID == schoolClass.ClassID && sc.SchoolID == schoolClass.SchoolID);


    if (existingSchoolClass == null)
    {
        // There is no existing record, so add the new SchoolClass
        _context.SchoolClasses.Add(schoolClass);
        _context.SaveChanges();
        return _context.SchoolClasses.FirstOrDefault(sc =>
        sc.ClassID == schoolClass.ClassID && sc.SchoolID == schoolClass.SchoolID);
;
    }
    else
    {
        // A record with the same ClassName already exists, return the existing one
        return existingSchoolClass;
    }
}


        public void UpdateSchoolClass(SchoolClass schoolClass)
        {
            _context.SchoolClasses.Update(schoolClass);
            _context.SaveChanges();
        }

        public void DeleteSchoolClass(int schoolClassId)
        {
            var schoolClass = GetSchoolClassById(schoolClassId);
            if (schoolClass != null)
            {
                _context.SchoolClasses.Remove(schoolClass);
                _context.SaveChanges();
            }
        }

        public int CountSchoolClassesByCriteria(int schoolId, int classId)
        {
            return _context.SchoolClasses.Count(sc => sc.SchoolID == schoolId && sc.ClassID == classId);
        }
    }

  
}
