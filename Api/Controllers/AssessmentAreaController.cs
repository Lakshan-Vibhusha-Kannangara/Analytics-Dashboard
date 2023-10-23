using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class AssessmentAreaController : ControllerBase
{
    private readonly SchoolDBContext _dbContext;

    public AssessmentAreaController(SchoolDBContext dbContext)
    {
        _dbContext = dbContext;
    }

  [HttpGet]

public IActionResult GetAssessmentAreaData([FromQuery] int assessmentAreaId){
        try
        {
            // Distinct Count of Students who answered in the AssessmentArea
            var distinctCount = _dbContext.Answers
                .Where(answer => answer.AssessmentAreaID == assessmentAreaId)
                .Select(answer => answer.StudentID)
                .Distinct()
                .Count();

            // Average Score for the AssessmentArea
            var averageScore = _dbContext.Answers
                .Where(answer => answer.AssessmentAreaID == assessmentAreaId)
                .Average(answer => answer.IsCorrect ? 1 : 0);

            // High Distinct Count (You may need to clarify what "high" means)
            int minAnswerCount = 5; // Adjust as needed

            // Step 1: Retrieve distinct student IDs
            var studentIdsInAssessmentArea = _dbContext.Answers
                .Where(a => a.AssessmentAreaID == assessmentAreaId)
                .Select(a => a.StudentID)
                .Distinct()
                .ToList();

            // Step 2: Query the StudentClass records based on student IDs
            var studentClasses = _dbContext.StudentClasses
                .Where(sc => studentIdsInAssessmentArea.Contains(sc.StudentID))
                .ToList();

            // Step 3: Group the StudentClass records by ClassroomID
            var groupedStudentClasses = studentClasses
                .GroupBy(sc => sc.ClassroomID);

            // Step 4: Count the groups with a certain condition
            int highDistinctCount = groupedStudentClasses
                .Count(group => group.Count() >= minAnswerCount);

            // Participant Count for the AssessmentArea
            var participantCount = _dbContext.Answers
                .Count(answer => answer.AssessmentAreaID == assessmentAreaId);

            // Correct Answer Percentage per Class
            var correctAnswerPercentagePerClass = _dbContext.Classes
                .Join(
                    _dbContext.StudentClasses,
                    cls => cls.ClassID,
                    sc => sc.ClassroomID,
                    (cls, sc) => new { Class = cls, StudentClass = sc }
                )
                .Join(
                    _dbContext.Answers,
                    combined => combined.StudentClass.StudentID,
                    ans => ans.StudentID,
                    (combined, ans) => new { Class = combined.Class, Answer = ans }
                )
                .Where(combined => combined.Answer.AssessmentAreaID == assessmentAreaId)
                .GroupBy(combined => combined.Class.ClassName)
                .Select(group => new
                {
                    ClassName = group.Key,
                    CorrectAnswerPercentage = Math.Round(
                        (double)group.Sum(combined => combined.Answer.IsCorrect ? 1 : 0) /
                        group.Count() * 100, 2) // Round to two decimal places
                })
                .ToList();

            // Sydney Average Score (Assuming Sydney is a specific Class or School)
            var sydneyClassId = 1; // Replace with the desired Class ID for Sydney
            var sydneyAverageScore = _dbContext.Answers
                .Where(answer => answer.AssessmentAreaID == assessmentAreaId && answer.Student.StudentClasses.Any(sc => sc.ClassroomID == sydneyClassId))
                .Average(answer => answer.IsCorrect ? 1 : 0);

            var sydneyParticipants = _dbContext.Answers
                .Count(answer => answer.AssessmentAreaID == assessmentAreaId && answer.Student.StudentClasses.Any(sc => sc.ClassroomID == sydneyClassId));

            // Create an object to hold the parameters
            var result = new
            {
                DistinctCount = distinctCount,
                AverageScore = Math.Round(averageScore * 100, 2), // Round to two decimal places
                HighDistinctCount = highDistinctCount,
                ParticipantCount = participantCount,
                CorrectAnswerPercentagePerClass = correctAnswerPercentagePerClass,
                SydneyAverageScore = Math.Round(sydneyAverageScore * 100, 2), // Round to two decimal places
                SydneyParticipants = sydneyParticipants
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
