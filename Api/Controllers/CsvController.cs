using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CsvImportController : ControllerBase
    {
        private readonly SchoolDBContext _context;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectClassRepository _subjectClassRepository;
        private readonly IAssessmentAreaRepository _assessmentAreaRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;

        private readonly ILogger<CsvImportController> _logger;
        public CsvImportController(
            SchoolDBContext context,
            IStudentRepository studentRepository,
            ISchoolRepository schoolRepository,
            ISubjectRepository subjectRepository,
            IClassRepository classRepository,
            ISubjectClassRepository subjectClassRepository,
            IAssessmentAreaRepository assessmentAreaRepository,
            IAnswerRepository answerRepository,
            IAwardRepository awardRepository,
            IStudentClassRepository studentClassRepository,
            ISchoolClassRepository schoolClassRepository,
            ILogger<CsvImportController> logger
        )
        {
            _context = context;
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
            _subjectRepository = subjectRepository;
            _classRepository = classRepository;
            _subjectClassRepository = subjectClassRepository;
            _assessmentAreaRepository = assessmentAreaRepository;
            _answerRepository = answerRepository;
            _awardRepository = awardRepository;
            _studentClassRepository = studentClassRepository;
            _schoolClassRepository = schoolClassRepository;
            _logger = logger;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCsvData([FromBody] CsvImportParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.FileId))
            {
                return BadRequest("FileId is a required parameter.");
            }

            try
            {

                var tempFilePath = Path.Combine(Path.GetTempPath(), "tempfile.csv");

                await DownloadCsvFileFromGoogleDrive(parameters.FileId, tempFilePath);

                using var reader = new StreamReader(tempFilePath);

                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ",",
                    BadDataFound = null
                });

                var records = csv.GetRecords<CsvRecord>().ToList();

                foreach (var record in records)


                {
                    var award = new Award
                    {
                        AwardName = record.award
                    };
                    award = _awardRepository.AddAward(award);
                 
                    var school = new School
                    {
                        SchoolName = record.school_name
                    };
                    school = _schoolRepository.AddSchool(school);

             
                    var classEntity = new Class
                    {
                        ClassName = record.Class,

                    };
                    classEntity = _classRepository.AddClass(classEntity);

              
                    var subject = new Subject
                    {
                        SubjectName = record.Subject,
                        SubjectScore = 0
                    };
                    subject = _subjectRepository.AddSubject(subject);

                 
                    var subjectClass = new SubjectClass
                    {
                        SubjectID = subject.SubjectID,
                        ClassID = classEntity.ClassID
                    };
                    subjectClass = _subjectClassRepository.AddSubjectClass(subjectClass);


                    var student = new Student
                    {
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                        YearLevel = record.YearLevel
                    };
                    student = _studentRepository.AddStudent(student);


                    var assessmentArea = new AssessmentArea
                    {
                        AreaName = record.AssessmentAreas
                    };
                    assessmentArea = _assessmentAreaRepository.AddAssessmentArea(assessmentArea);

                    var studentClass = new StudentClass
                    {
                        StudentID = student.StudentID,
                        ClassroomID = classEntity.ClassID
                    };
                    studentClass = _studentClassRepository.AddStudentClass(studentClass);


                    var schoolClass = new SchoolClass
                    {
                        SchoolID = school.SchoolID,
                        ClassID = classEntity.ClassID
                    };
                    schoolClass = _schoolClassRepository.AddSchoolClass(schoolClass);
                    var answer = new Answer
                    {
                        AnswerText = record.Answers,
                        StudentID = student.StudentID,
                        AssessmentAreaID = assessmentArea.AreaID,
                        CorrectAnswerText = record.CorrectAnswers,
                        IsCorrect = false
                    };
                    answer = _answerRepository.AddAnswer(answer);


                }

                _context.SaveChanges();

                return Ok("CSV data imported successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while importing CSV data: " + ex.Message);
            }
        }
        private async Task DownloadCsvFileFromGoogleDrive(string fileId, string savePath)
        {
            using (var client = new WebClient())
            {
                try
                {
                    var downloadUrl = $"https://drive.google.com/uc?id={fileId}";
                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), savePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An exception occurred during a WebClient request.");
                    throw; 
                }
            }
        }
    }

    public class CsvImportParameters
    {
        public string FileId { get; set; } 
  
    }
}
