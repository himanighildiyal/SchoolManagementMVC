using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Services.Interfaces
{
    public interface IStudentService
    {
        List<StudentDto> GetStudents();

        StudentDto GetStudentById(int studentId);

        bool Add(StudentDto model);

        void Delete(int studentId);

        bool Update(StudentDto model);
    }
}
