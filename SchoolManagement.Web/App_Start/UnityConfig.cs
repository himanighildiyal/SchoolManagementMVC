using SchoolManagement.Services;
using SchoolManagement.Services.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace SchoolManagement.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IClassService, ClassService>();
            container.RegisterType<IStudentService, StudentService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}