using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly SchoolDBContext _context;

    public StudentController(SchoolDBContext context)
    {
        _context = context;
    }

    [HttpGet("GetStudentInformation")]
      
    public IActionResult GetStudentInformation([FromQuery] int studentID)
    {
    

        // Retrieve the student by ID
        var student = _context.Students
            .Include(s => s.Answers)
            .Include(s => s.StudentClasses)
            .ThenInclude(sc => sc.Class)
            .ThenInclude(c => c.SubjectClasses)
            .ThenInclude(s => s.Subject)
            .FirstOrDefault(s => s.StudentID == studentID);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        // Calculate the assessment score for each assessment area
   // Retrieve assessment scores
var assessmentScores = _context.AssessmentAreas
    .Where(aa => _context.Answers.Any(a => a.AssessmentAreaID == aa.AreaID && a.StudentID == student.StudentID))
    .Select(aa => new
    {   assessment_id=aa.AreaID,
       assessment_name= aa.AreaName,
        score= _context.Answers
            .Where(a => a.AssessmentAreaID == aa.AreaID && a.StudentID == student.StudentID && a.IsCorrect == true)
            .Count(),
       total_questions = _context.Answers
            .Where(a => a.AssessmentAreaID == aa.AreaID && a.StudentID == student.StudentID)
            .Count(),
        correct_answer_percentage_class= (double)_context.Answers
            .Where(a => a.AssessmentAreaID == aa.AreaID && a.StudentID == student.StudentID && a.IsCorrect == true)
            .Count() * 100.0 /
            (double)_context.Answers
            .Where(a => a.AssessmentAreaID == aa.AreaID && a.StudentID == student.StudentID)
            .Count()
    })
    .ToList();


        // Calculate the total score for the student
        double studentScore = assessmentScores.Sum(area => area.score);

        int totalQuestionsAttempted = student.Answers?.Count ?? 0;
        int yearLevel = student.YearLevel;

        int distinctionCount = CalculateDistinctionCount(student);

        int participationStatus = student.Answers.Any() ? 1 : 0;
        var classNames = student.StudentClasses?.Select(sc => sc.Class?.ClassName).ToList();
        var subjectNames = student.StudentClasses?.SelectMany(sc => sc.Class?.SubjectClasses?.Select(s => s.Subject?.SubjectName)).ToList();

        // Query the school information based on your database structure
        var schoolName = _context.SchoolClasses
            .Where(sc => sc.Class.ClassID == student.StudentClasses.FirstOrDefault().Class.ClassID)
            .Select(sc => sc.School.SchoolName)
            .FirstOrDefault();

        // Calculate the correct answer percentage
        string correctAnswerPercentage = CalculateCorrectAnswerPercentage(student);

        var result = new
        {
            AssessmentScores = assessmentScores,
            StudentScore = studentScore,
            TotalQuestionsAttempted = totalQuestionsAttempted,
            YearLevel = yearLevel,
            Class = classNames,
            Subject = subjectNames,
            DistinctionCount = distinctionCount,
            ParticipationStatus = participationStatus,
            SchoolName = schoolName,
            CorrectAnswerPercentage = correctAnswerPercentage ,
            SydneyPercentage=CalculateSydneyPercentile(student.StudentID)
        };

        return Ok(result);
    }

    // Helper method to calculate the assessment score for a specific assessment area
    private static double CalculateAssessmentScore(Student student, int areaID)
    {
        var areaAnswers = student.Answers.Where(answer => answer.AssessmentAreaID == areaID);
        return areaAnswers.Sum(answer => answer.IsCorrect ? 1 : 0);
    }

    // Helper method to calculate the distinction count for a student
    private int CalculateDistinctionCount(Student student)
    {
        // Implement your logic to calculate the distinction count here.
        return 0; // Placeholder value, replace with your actual logic.
    }

    // Helper method to calculate the correct answer percentage for a student
 private string CalculateCorrectAnswerPercentage(Student student)
{
    int totalAnswers = student.Answers?.Count ?? 0;
    if (totalAnswers == 0)
    {
        return "0.00%";
    }

    int correctAnswers = student.Answers.Count(answer => answer.IsCorrect);
    double percentage = (correctAnswers * 100.0) / totalAnswers;
    return percentage.ToString("0.00") + "%";
} public string CalculateSydneyPercentile(int studentID)
    {
        // Step 1: Calculate the student's score based on the number of correct answers.
        int studentScore = _context.Answers
            .Count(a => a.StudentID == studentID && a.IsCorrect);

        // Step 2: Retrieve the number of correct answers for all students.
        var allStudentScores = _context.Answers
            .Where(a => a.IsCorrect)
            .GroupBy(a => a.StudentID)
            .Select(g => new
            {
                StudentID = g.Key,
                Score = g.Count()
            })
            .ToList();

        // Step 3: Sort the scores in ascending order.
        allStudentScores.Sort((a, b) => a.Score.CompareTo(b.Score));

        // Step 4: Find the position of the student's score within the sorted list.
        int studentPosition = allStudentScores.FindIndex(s => s.StudentID == studentID);

        // Step 5: Calculate the percentile based on the position.
        double percentile = (double)(studentPosition + 1) / allStudentScores.Count * 100;

      string formattedPercentile = percentile.ToString("F2")+ "%";

        return formattedPercentile;
    }

    public class StudentRequest
    {
        public int StudentID { get; set; }
    }
}

