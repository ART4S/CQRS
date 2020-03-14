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
                .Where(t => t.GetInterfaces().Any(i => i == typeof(IHasMappings)));

            foreach (var type in mapTypes)
            {
                var instance = (IHasMappings)Activator.CreateInstance(type);
                instance.ApplyMappings(this);
            }
        }
    }
}
