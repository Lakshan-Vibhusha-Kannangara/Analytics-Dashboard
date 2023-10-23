using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SchoolController : ControllerBase
{
    private readonly SchoolDBContext _context;

    public SchoolController(SchoolDBContext context)
    {
        _context = context;
    }


[HttpGet("school-info")]

public async Task<ActionResult<SchoolInfo>> GetSchoolInfo([FromQuery] int schoolId)
{
    try
    {
        var schoolInfo = await _context.Schools
            .Where(s => s.SchoolID == schoolId)
            .Select(school => new SchoolInfo
            {
                SchoolName = school.SchoolName,
                StudentInfos = _context.Students
                    .Where(st => st.StudentClasses.Any(stc => stc.Class.SchoolClasses.Any(schoolClass => schoolClass.SchoolID == schoolId)))
                    .Select(s => new StudentInfo
                    {
                        StudentID = s.StudentID,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        YearLevel = s.YearLevel
                    })
                    .OrderBy(s => s.StudentID)
                    .ToList(),
                ClassInfos = _context.Classes
                    .Where(c => c.SubjectClasses.Any(sc => sc.Class.SchoolClasses.Any(schoolClass => schoolClass.SchoolID == schoolId)))
                    .Select(c => new ClassInfo
                    {
                        ClassID = c.ClassID,
                        ClassName = c.ClassName
                    })
                    .ToList(),
            })
            .FirstOrDefaultAsync();

        if (schoolInfo == null)
        {
            return NotFound(); // School with the given ID was not found.
        }

        return Ok(schoolInfo);
    }
    catch (Exception ex)
    {
        // Handle exceptions appropriately, e.g., log them and return an error response.
        return StatusCode(500, $"An error occurred: {ex.Message}");
    }
}

    public class SchoolInfo
    {
        public List<StudentInfo> StudentInfos { get; set; }
        public List<ClassInfo> ClassInfos { get; set; }
        public int StudentCount { get; set; }
        public string? SchoolName { get; internal set; }
    }

    public class StudentInfo
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearLevel { get; set; }
    }

    public class ClassInfo
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
    }
}
