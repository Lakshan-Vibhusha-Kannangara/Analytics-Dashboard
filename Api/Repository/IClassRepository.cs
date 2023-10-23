using API.Models;
using System;
using System.Collections.Generic;

namespace API.Repository
{
    public interface IClassRepository
    {
        IEnumerable<Class> GetAllClasses();
        Class GetClassById(int classId);
        Class AddClass(Class classEntity);
        void UpdateClass(Class classEntity);
        void DeleteClass(int classId);
    }
}
