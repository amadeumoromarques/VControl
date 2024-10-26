using NetCore.AutoRegisterDi;

namespace Marques.AI.Infra
{
    public static class ModuleConfigExtensions
    {
        public static void AddAutoRegister<T>(this IServiceCollection services)
        {
            services.AddAutoRegister<T>("Repository");
            services.AddAutoRegister<T>("Service");
            services.AddAutoRegister<T>("Provider");
        }

        public static void AddAutoRegister<T>(this IServiceCollection services, string classesEndedWith)
        {
            (from c in services.RegisterAssemblyPublicNonGenericClasses(typeof(T).Assembly)
             where c.Name.EndsWith(classesEndedWith)
             select c).AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
        }
    }
}