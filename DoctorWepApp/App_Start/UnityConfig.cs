using DoctorWepApp.Repositories.Impl;
using DoctorWepApp.Repositories.Interfaces;
using DoctorWepApp.Services.Impl;
using DoctorWepApp.Services.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace DoctorWepApp
{

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IDoctorRepository, DoctorRepository>();
            container.RegisterType<IDoctorService, DoctorService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
