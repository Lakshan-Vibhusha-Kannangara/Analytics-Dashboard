using CsvHelper.Configuration.Attributes;

public class CsvRecord
{
    public string school_name { get; set; }
    public int year { get; set; }
    public int StudentID { get; set; }
     [Name("First Name")] 
    public string FirstName { get; set; }

    [Name("Last Name")] 
    public string LastName { get; set; }
 [Name("Year Level")] 
    public int YearLevel { get; set; }
    public string Class { get; set; }
    public string Subject { get; set; }
    public string Answers { get; set; }
      [Name("Correct Answers")] 
    public string CorrectAnswers { get; set; }
      [Name("Question Number")] 
    public int QuestionNumber { get; set; }
          [Name("Subject Contents")] 

    public string SubjectContents { get; set; }
     [Name("Assessment Areas")] 
    public string AssessmentAreas { get; set; }
    public decimal sydney_correct_count_percentage { get; set; }
    public decimal sydney_average_score { get; set; }
    public int sydney_participants { get; set; }
    public decimal student_score { get; set; }
    public decimal student_total_assessed { get; set; }
    public decimal student_area_assessed_score { get; set; }
    public decimal total_area_assessed_score { get; set; }
    public string participant { get; set; }
    public decimal correct_answer_percentage_per_class { get; set; }
    public decimal average_score { get; set; }
    public int school_percentile { get; set; }
    public int sydney_percentile { get; set; }
    public string strength_status { get; set; }
      [Name("award")] 
    public string award{ get; set; }
    public int high_distinct_count { get; set;}
}
