using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface IAwardRepository
    {
        IEnumerable<Award> GetAllAwards();
        Award GetAwardById(int awardId);
        Award AddAward(Award award);
        void UpdateAward(Award award);
        void DeleteAward(int awardId);
    }

    public class AwardRepository : IAwardRepository
    {
        private readonly SchoolDBContext _context;

        public AwardRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Award> GetAllAwards()
        {
            return _context.Awards.ToList();
        }

        public Award GetAwardById(int awardId)
        {
            try
            {
                return _context.Awards.FirstOrDefault(award => award.AwardID == awardId);
            }
            catch
            {
                return null;
            }
        }

     public Award AddAward(Award award)
{

    var existingAward = _context.Awards.FirstOrDefault(a => a.AwardName == award.AwardName);

    if (existingAward == null)
    {
        _context.Awards.Add(award);
        _context.SaveChanges();
        return _context.Awards.FirstOrDefault(a => a.AwardName == award.AwardName);
    }
    else
    {
        return award;
}
    }

  


        public void UpdateAward(Award award)
        {
            _context.Awards.Update(award);
            _context.SaveChanges();
        }

        public void DeleteAward(int awardId)
        {
            var award = GetAwardById(awardId);
            if (award != null)
            {
                _context.Awards.Remove(award);
                _context.SaveChanges();
            }
        }

     
    }
}
