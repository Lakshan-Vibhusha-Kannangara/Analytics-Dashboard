using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface IAssessmentAreaRepository
    {
        IEnumerable<AssessmentArea> GetAllAssessmentAreas();
        AssessmentArea GetAssessmentAreaById(int assessmentAreaId);
        AssessmentArea AddAssessmentArea(AssessmentArea assessmentArea);
        void UpdateAssessmentArea(AssessmentArea assessmentArea);
        void DeleteAssessmentArea(int assessmentAreaId);
        int CountAssessmentAreasByCriteria(string areaName);
    }
}
