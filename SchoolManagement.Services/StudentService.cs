using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDBEntities _dbContext;

        public StudentService()
        {
            _dbContext = new SchoolDBEntities();
        }

        public List<StudentDto> GetStudents()
        {
            List<StudentDto> result = new List<StudentDto>();

            var entities = _dbContext.Students.Where(x => x.IsDeleted == false);

            foreach (var entity in entities)
            {
                StudentDto temp = new StudentDto();

                temp.StudentId = entity.StudentId;
                temp.FirstName = entity.FirstName;
                temp.LastName = entity.LastName;
                temp.MobileNumber = entity.MobileNo;
                temp.Address = entity.Address;
                temp.State = entity.State;
                temp.City = entity.City;
                temp.ClassId = entity.ClassId;
                temp.CreatedOn = entity.CreatedOn;
                var Classes = _dbContext.SchoolClasses.FirstOrDefault(x => x.ClassId == temp.ClassId);
                temp.ClassName = Classes.ClassName;

                result.Add(temp);
            }

            return result;
        }

        public bool Add(StudentDto model)
        {
            bool result = false;

            try
            {
                Student entity = new Student();

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.MobileNo = model.MobileNumber;
                entity.Address = model.Address;
                entity.State = model.State;
                entity.City = model.City;
                entity.ClassId = model.ClassId;
                entity.IsDeleted = false;
                entity.CreatedOn = DateTime.Now;

                _dbContext.Students.Add(entity);

                int count = _dbContext.SaveChanges();

                if (count > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public StudentDto GetStudentById(int studentId)
        {
            StudentDto result;

            var entity = _dbContext.Students.FirstOrDefault(x => x.StudentId == studentId && x.IsDeleted == false);

            if (entity != null)
            {
                result = new StudentDto();

                result.StudentId = entity.StudentId;
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                result.City = entity.City;
                result.State = entity.State;
                result.MobileNumber = entity.MobileNo;
                result.CreatedOn = entity.CreatedOn;

                result.ClassId = entity.ClassId;
                result.ClassName = entity.SchoolClass.ClassName;

                result.Address = entity.Address;
            }
            else
            {
                result = null;
            }

            return result;
        }

        public void Delete(int studentId)
        {
            var entity = _dbContext.Students.FirstOrDefault(x => x.StudentId == studentId && x.IsDeleted == false);

            if (entity != null)
            {
                entity.IsDeleted = true;

                _dbContext.SaveChanges();
            }
        }

        public bool Update(StudentDto model)
        {
            bool result = true;

            try
            {
                var entity = _dbContext.Students.FirstOrDefault(x => x.StudentId == model.StudentId && x.IsDeleted == false);

                if (entity != null)
                {
                    entity.FirstName = model.FirstName;
                    entity.LastName = model.LastName;
                    entity.MobileNo = model.MobileNumber;
                    entity.Address = model.Address;
                    entity.State = model.State;
                    entity.City = model.City;
                    entity.ClassId = model.ClassId;

                    int count = _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
