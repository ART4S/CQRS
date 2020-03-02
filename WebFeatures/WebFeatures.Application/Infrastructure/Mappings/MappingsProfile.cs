using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Application.Infrastructure.Mappings
{
    internal class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            AddMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void AddMappingsFromAssembly(Assembly assembly)
        {
            var mapTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(x => x == typeof(IHasMappings)));

            foreach (var type in mapTypes)
            {
                MethodInfo method = type.GetMethod(nameof(IHasMappings.ApplyMappings));

                object instance = Activator.CreateInstance(type);
                method?.Invoke(instance, new object[] {this});
            }
        }
    }
}
