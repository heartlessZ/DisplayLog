using DisplayLog.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SV.WebExtension.Core
{
    public static class AutoFactoryExtensions
    {
        public static IServiceCollection AddAutoFactory(this IServiceCollection services, string serviceAssemblyName)
        {
            var serviceAssembly = Assembly.Load(serviceAssemblyName);
            var baseType = typeof(IDependency);
            var types = serviceAssembly.GetTypes();
            types.Where(x => ((TypeInfo)x).ImplementedInterfaces.Contains(baseType))
                .ToList().ForEach(type =>
                {
                    if (type.IsInterface)
                    {
                        var impType = types.FirstOrDefault(x => ((TypeInfo)x).ImplementedInterfaces.Contains(type) && x.IsClass);
                        if (impType != null)
                            services.AddScoped(type, impType);
                    }
                    else if (type.IsClass)
                    {
                        services.AddScoped(type);
                    }
                });
            return services;
        }

        public static IServiceCollection AddAutoFactory(this IServiceCollection services, IEnumerable<string> serviceAssemblyName)
        {
            foreach (var assemblyName in serviceAssemblyName)
            {
                AddAutoFactory(services, assemblyName);
            }

            return services;
        }
    }
}
