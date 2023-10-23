

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class School
{
    [Key]
    public int SchoolID { get; set; }
    public string? SchoolName { get; set; }

    public List<Class>? Classes { get; set; }
}

public class Subject
{
    [Key]
    public int SubjectID { get; set; }
    public string? SubjectName { get; set; }
    public int SubjectScore { get; set; }

    public List<SubjectClass>? SubjectClasses { get; set; }
}

public class Class
{
    [Key]
    public int ClassID { get; set; }
    public string? ClassName { get; set; }

    public List<SubjectClass>? SubjectClasses { get; set; }
    public List<SchoolClass>? SchoolClasses { get; set; } // Ensure this property is defined
}

public class SchoolClass
{
    [Key]
    public int ID { get; set; }

    [ForeignKey("School")]
    public int SchoolID { get; set; }
    public School School { get; set; }

    [ForeignKey("Class")]
    public int ClassID { get; set; }
    public Class Class { get; set; }
}

public class SubjectClass
{
    [Key]
    public int SubjectClassID { get; set; }

    [ForeignKey("Subject")]
    public int SubjectID { get; set; }
    public Subject? Subject { get; set; }

    [ForeignKey("Class")]
    public int ClassID { get; set; }
    public Class? Class { get; set; }
}

public class AssessmentArea
{
    [Key]
    public int AreaID { get; set; }
    public string? AreaName { get; set; }

    public List<Answer>? Answers { get; set; }
}

public class Student
{
    [Key]
    public int StudentID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int YearLevel { get; set; }

    public List<StudentClass>? StudentClasses { get; set; }
    public List<Answer>? Answers { get; set; }
}

public class StudentClass
{
    [Key]
    public int ID { get; set; }

    [ForeignKey("Student")]
    public int StudentID { get; set; }
    public Student Student { get; set; }

    [ForeignKey("Class")]
    public int ClassroomID { get; set; }
    public Class Class { get; set; }
}
public class Answer
{
    [Key]
    public int AnswerID { get; set; }
    public string? AnswerText { get; set; }
    public string? CorrectAnswerText { get; set; }

    [ForeignKey("AssessmentArea")]
    public int AssessmentAreaID { get; set; }
    public AssessmentArea? AssessmentArea { get; set; }

    [ForeignKey("Student")]
    public int StudentID { get; set; }
    public Student? Student { get; set; }

    public bool IsCorrect { get; set; }
}
public class Award
{
    [Key]
    public int AwardID { get; set; }
    public string? AwardName { get; set; }
}