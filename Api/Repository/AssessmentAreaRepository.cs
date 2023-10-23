using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class AssessmentAreaRepository : IAssessmentAreaRepository
    {
        private readonly SchoolDBContext _context;

        public AssessmentAreaRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<AssessmentArea> GetAllAssessmentAreas()
        {
            return _context.AssessmentAreas.ToList();
        }

        public AssessmentArea GetAssessmentAreaById(int assessmentAreaId)
        {
            try
            {
                return _context.AssessmentAreas.FirstOrDefault(area => area.AreaID == assessmentAreaId);
            }
            catch
            {
                return null;
            }
        }

   public AssessmentArea AddAssessmentArea(AssessmentArea assessmentArea)
{
  var  found = _context.AssessmentAreas.FirstOrDefault(area => area.AreaName == assessmentArea.AreaName);


    if (found == null)
    {
        _context.AssessmentAreas.Add(assessmentArea);
       _context.SaveChanges();
       return _context.AssessmentAreas.FirstOrDefault(area => area.AreaName == assessmentArea.AreaName);

    }
    else
    {
     return found;
    }
}


        public void UpdateAssessmentArea(AssessmentArea assessmentArea)
        {
            _context.AssessmentAreas.Update(assessmentArea);
            _context.SaveChanges();
        }

        public void DeleteAssessmentArea(int assessmentAreaId)
        {
            var assessmentArea = GetAssessmentAreaById(assessmentAreaId);
            if (assessmentArea != null)
            {
                _context.AssessmentAreas.Remove(assessmentArea);
                _context.SaveChanges();
            }
        }

        public int CountAssessmentAreasByCriteria(string areaName)
        {
            return _context.AssessmentAreas.Count(area => area.AreaName == areaName);
        }
    }

   
}
