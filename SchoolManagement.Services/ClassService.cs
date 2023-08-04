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
    public class ClassService : IClassService
    {
        private readonly SchoolDBEntities _dbContext;

        public ClassService()
        {
            _dbContext = new SchoolDBEntities();
        }

        public bool AddClass(ClassDto model)
        {
            bool result = false;

            try
            {
                SchoolClass entity = new SchoolClass();

                entity.ClassName = model.ClassName;
                entity.IsDeleted = false;
                entity.CreatedOn = DateTime.Now;

                _dbContext.SchoolClasses.Add(entity);

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

        public void DeleteClass(int classId)
        {
            try
            {
                var entity = _dbContext.SchoolClasses.FirstOrDefault(x => x.ClassId == classId && x.IsDeleted == false);

                if (entity != null)
                {
                    entity.IsDeleted = true;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void UpdateClass(ClassDto model)
        {
            try
            {
                var entity = _dbContext.SchoolClasses.FirstOrDefault(x => x.ClassId == model.ClassId && x.IsDeleted == false);

                if (entity != null)
                {
                    entity.ClassName = model.ClassName;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public ClassDto GetClassById(int classId)
        {
            ClassDto result;

            var entity = _dbContext.SchoolClasses.FirstOrDefault(x => x.ClassId == classId && x.IsDeleted == false);

            if (entity != null)
            {
                result = new ClassDto();

                result.ClassId = entity.ClassId;
                result.ClassName = entity.ClassName;
                result.CreatedOn = entity.CreatedOn;
            }
            else
            {
                result = null;
            }

            return result;
        }

        public List<ClassDto> GetClasses()
        {
            List<ClassDto> result = new List<ClassDto>();

            var entities = _dbContext.SchoolClasses.Where(x => x.IsDeleted == false);

            foreach (var entity in entities)
            {
                ClassDto temp = new ClassDto();

                temp.ClassId = entity.ClassId;
                temp.ClassName = entity.ClassName;
                temp.CreatedOn = entity.CreatedOn;

                result.Add(temp);
            }

            return result;
        }

        public bool CheckClassNameExist(int classId, string className)
        {
            bool result = false;

            var entity = _dbContext.SchoolClasses.FirstOrDefault(x => x.ClassId != classId && x.ClassName.ToLower() == className.ToLower()
                                && x.IsDeleted == false);

            if (entity != null)
            {
                result = true;
            }

            return result;
        }

    }
}
