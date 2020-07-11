using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace WebFeatures.Application.Infrastructure.Mappings
{
	internal class MappingsProfile : Profile
	{
		public MappingsProfile()
		{
			AddMappingsFromAssembly(typeof(MappingsProfile).Assembly);
		}

		private void AddMappingsFromAssembly(Assembly assembly)
		{
			IEnumerable<Type> mapTypes = assembly.GetExportedTypes()
			   .Where(t => t.GetInterfaces().Any(i => i == typeof(IHasMappings)));

			foreach (Type type in mapTypes)
			{
				IHasMappings instance = (IHasMappings) Activator.CreateInstance(type);

				instance.ApplyMappings(this);
			}
		}
	}
}
