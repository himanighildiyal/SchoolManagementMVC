using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Services.Interfaces
{
    public interface IClassService
    {
        bool AddClass(ClassDto model);

        void DeleteClass(int classId);

        void UpdateClass(ClassDto model);

        ClassDto GetClassById(int classId);

        List<ClassDto> GetClasses();

        bool CheckClassNameExist(int classId, string className);
    }
}
