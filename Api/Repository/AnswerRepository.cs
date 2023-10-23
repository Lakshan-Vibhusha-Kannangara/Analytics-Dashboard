using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAllAnswers();
        Answer GetAnswerById(int answerId);
        Answer AddAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void DeleteAnswer(int answerId);
    }

    public class AnswerRepository : IAnswerRepository
    {
        private readonly SchoolDBContext _context;

        public AnswerRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Answer> GetAllAnswers()
        {
            return _context.Answers.ToList();
        }

        public Answer GetAnswerById(int answerId)
        {
            try
            {
                return _context.Answers.FirstOrDefault(answer => answer.AnswerID == answerId);
            }
            catch
            {
                return null;
            }
        }

public Answer AddAnswer(Answer answer)
{
    // Check if there are no existing answers with the same criteria
    var existingAnswer = _context.Answers.FirstOrDefault(a =>
        a.AnswerText == answer.AnswerText &&
        a.AssessmentAreaID == answer.AssessmentAreaID &&
        a.StudentID == answer.StudentID);

    if (existingAnswer == null)
    {
        _context.Answers.Add(answer);
        _context.SaveChanges();

        // Return the newly added answer
        return answer;
    }
    else
    {
        // Return the existing answer
        return existingAnswer;
    }
}


        public void UpdateAnswer(Answer answer)
        {
            _context.Answers.Update(answer);
            _context.SaveChanges();
        }

        public void DeleteAnswer(int answerId)
        {
            var answer = GetAnswerById(answerId);
            if (answer != null)
            {
                _context.Answers.Remove(answer);
                _context.SaveChanges();
            }
        }
    }
}
