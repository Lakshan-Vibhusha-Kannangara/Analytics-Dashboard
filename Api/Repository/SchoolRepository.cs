using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SchoolDBContext _context;

        public SchoolRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<School> GetAllSchools()
        {
            return _context.Schools.ToList();
        }

        public School GetSchoolById(int schoolId)
        {
            try
            {
                return _context.Schools.FirstOrDefault(school => school.SchoolID == schoolId);
            }
            catch
            {
                return null;
            }
        }

public School AddSchool(School school)
{
    // Check if there are no existing schools with the same name
    var existingSchool = _context.Schools.FirstOrDefault(s => s.SchoolName == school.SchoolName);

    if (existingSchool == null)
    {
        _context.Schools.Add(school);
        _context.SaveChanges();
        return _context.Schools.FirstOrDefault(s => s.SchoolName == school.SchoolName); 
    }
    else
    {
       return existingSchool;
    }
}



        public void UpdateSchool(School school)
        {
            _context.Schools.Update(school);
            _context.SaveChanges();
        }

        public void DeleteSchool(int schoolId)
        {
            var school = GetSchoolById(schoolId);
            if (school != null)
            {
                _context.Schools.Remove(school);
                _context.SaveChanges();
            }
        }

        public int CountSchoolsByCriteria(string schoolName)
        {
            return _context.Schools
                .Count(school => school.SchoolName == schoolName);
        }

     
    }
}
