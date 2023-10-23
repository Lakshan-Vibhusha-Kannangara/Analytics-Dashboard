using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{


    public class ClassRepository : IClassRepository
    {
        private readonly SchoolDBContext _context;

        public ClassRepository(SchoolDBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Class> GetAllClasses()
        {
            return _context.Classes.ToList();
        }

        public Class GetClassById(int classId)
        {
            try
            {
                return _context.Classes.FirstOrDefault(classEntity => classEntity.ClassID == classId);
            }
            catch
            {
                return null;
            }
        }

      public Class AddClass(Class classEntity)
{
    var existingClass = _context.Classes.FirstOrDefault(c => c.ClassName == classEntity.ClassName );

    if (existingClass == null)
    {
        _context.Classes.Add(classEntity);
        _context.SaveChanges();
        return _context.Classes.FirstOrDefault(c => c.ClassName == classEntity.ClassName);

    }
    else
    {
      return existingClass;
    }
}


        public void UpdateClass(Class classEntity)
        {
            _context.Classes.Update(classEntity);
            _context.SaveChanges();
        }

        public void DeleteClass(int classId)
        {
            var classEntity = GetClassById(classId);
            if (classEntity != null)
            {
                _context.Classes.Remove(classEntity);
                _context.SaveChanges();
            }
        }

        public int CountClassesByCriteria(string className)
        {
            return _context.Classes.Count(classEntity => classEntity.ClassName == className);
        }

    
    }
}
