using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SearchController: ControllerBase
{
    private readonly SchoolDBContext _context;

    public SearchController(SchoolDBContext context)
    {
        _context = context;
    }

   [HttpGet("Search")]

public async Task<ActionResult<IEnumerable<object>>> SearchStudentByName(string name)
{
    if (string.IsNullOrEmpty(name))
    {
        return BadRequest("Name parameter is required.");
    }

    var students = await _context.Students
        .Where(s => s.FirstName.Contains(name) || s.LastName.Contains(name))
        .Select(s => new
        {
            StudentID = s.StudentID,
            FullName = s.FirstName + " " + s.LastName
        })
        .Take(6) // Limit the result to the first 6 items
        .ToListAsync();

    if (students.Count == 0)
    {
        return NotFound("No students found with the given name.");
    }

    return Ok(students);
}
[HttpGet("SearchAssessmentArea")]

public async Task<ActionResult<IEnumerable<AssessmentArea>>> SearchAssessmentAreaByName(string name)
{
    if (string.IsNullOrEmpty(name))
    {
        return BadRequest("Name parameter is required.");
    }

    var assessmentAreas = await _context.AssessmentAreas
        .Where(a => a.AreaName.Contains(name))
        .Take(6)
        .ToListAsync();

    if (assessmentAreas.Count == 0)
    {
        return NotFound("No assessment areas found with the given name.");
    }

    return Ok(assessmentAreas);
}
[HttpGet("SearchSchool")]

public async Task<ActionResult<IEnumerable<object>>> SearchSchoolByName(string schoolName)
{
    if (string.IsNullOrEmpty(schoolName))
    {
        return BadRequest("School name parameter is required.");
    }

    var schools = await _context.Schools
        .Where(s => s.SchoolName.Contains(schoolName))
        .Select(s => new
        {
            SchoolId = s.SchoolID,
            SchoolName = s.SchoolName
        })
        .ToListAsync();

    if (schools.Count == 0)
    {
        return NotFound("No schools found with the given name.");
    }

    return Ok(schools);
}

}
